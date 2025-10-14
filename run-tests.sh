#!/bin/bash

# CandyMapper BDD Test Execution Script

echo "========================================"
echo "CandyMapper BDD Test Framework"
echo "========================================"

# Function to display help
show_help() {
    echo "Usage: ./run-tests.sh [OPTIONS]"
    echo ""
    echo "Options:"
    echo "  -a, --all           Run all tests"
    echo "  -s, --smoke         Run smoke tests only"
    echo "  -r, --regression    Run regression tests only"
    echo "  -i, --integration   Run integration tests only"
    echo "  -c, --config        Run configuration tests only"
    echo "  -v, --verbose       Run with verbose output"
    echo "  -h, --help          Show this help message"
    echo ""
    echo "Examples:"
    echo "  ./run-tests.sh --smoke"
    echo "  ./run-tests.sh --all --verbose"
    echo "  ./run-tests.sh --regression"
}

# Default values
TEST_FILTER=""
VERBOSE=""

# Parse command line arguments
while [[ $# -gt 0 ]]; do
    case $1 in
        -a|--all)
            TEST_FILTER=""
            shift
            ;;
        -s|--smoke)
            TEST_FILTER="--filter TestCategory=smoke"
            shift
            ;;
        -r|--regression)
            TEST_FILTER="--filter TestCategory=regression"
            shift
            ;;
        -i|--integration)
            TEST_FILTER="--filter TestCategory=integration"
            shift
            ;;
        -c|--config)
            TEST_FILTER="--filter TestCategory=configuration"
            shift
            ;;
        -v|--verbose)
            VERBOSE="--verbosity normal"
            shift
            ;;
        -h|--help)
            show_help
            exit 0
            ;;
        *)
            echo "Unknown option: $1"
            show_help
            exit 1
            ;;
    esac
done

echo "Starting test execution..."
echo "Date: $(date)"
echo ""

# Restore packages
echo "Restoring NuGet packages..."
dotnet restore

if [ $? -ne 0 ]; then
    echo "Failed to restore packages"
    exit 1
fi

# Build the project
echo "Building project..."
dotnet build

if [ $? -ne 0 ]; then
    echo "Build failed"
    exit 1
fi

# Run tests
echo "Running tests..."
if [ -z "$TEST_FILTER" ]; then
    echo "Running all tests"
    dotnet test $VERBOSE
else
    echo "Running filtered tests: $TEST_FILTER"
    dotnet test $TEST_FILTER $VERBOSE
fi

TEST_RESULT=$?

echo ""
echo "========================================"
if [ $TEST_RESULT -eq 0 ]; then
    echo "✅ Tests completed successfully!"
else
    echo "❌ Tests failed with exit code: $TEST_RESULT"
fi
echo "========================================"

exit $TEST_RESULT