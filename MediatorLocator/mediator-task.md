# Task: Implement a Request Handler Mediator Library

## Objective
Implement a lightweight **Mediator Pattern** library in C# that facilitates command handling by decoupling the sender of a request from its receiver.  This promotes cleaner, more maintainable code architecture. 

---

## Requirements

### 1. Core Interfaces

#### **ICommand<TParam, TResult>**
- Create a marker interface that represents a command/request
- Generic parameters: 
  - `TParam`: The input parameter type for the command
  - `TResult`: The return type after command execution

#### **IHandler<TCommand, TParam, TResult>**
- Create an interface for command handlers
- Must have a constraint where `TCommand` implements `ICommand<TParam, TResult>`
- Must contain a single method: `TResult Handle(TParam param)`

#### **IMediator**
- Create an interface with two methods:
  1. **Register**:  Registers a handler for a specific command type
     - Signature: `void Register<TCommand, T, TResult>(IHandler<TCommand, T, TResult> handler) where TCommand : ICommand<T, TResult>`
  2. **Send**: Sends a command to its registered handler and returns the result
     - Signature: `TResult Send<T, TResult>(ICommand<T, TResult> command, T param)`

---

### 2. Mediator Implementation

Create a concrete **Mediator** class that implements `IMediator`:

**Functionality:**
- Maintain an internal dictionary to store command type → handler mappings
- **Register Method:**
  - Store the handler for a specific command type
  - Throw `HandlerAlreadyRegisteredException` if trying to register the same command type twice
- **Send Method:**
  - Look up the handler for the given command type
  - Execute the handler's `Handle` method with the provided parameter
  - Return the result
  - Throw `HandlerNotRegisteredException` if no handler is registered for the command type

**Implementation Tips:**
- Use `Dictionary<Type, Delegate>` for storing handlers
- Use `typeof(TCommand)` to get the command type
- Store the handler's `Handle` method as a `Func<T, TResult>` delegate

---

### 3. Custom Exceptions

Create two custom exception classes: 

#### **HandlerAlreadyRegisteredException**
- Inherits from `Exception`
- Should have three constructors:
  - Default constructor
  - Constructor with message
  - Constructor with message and inner exception

#### **HandlerNotRegisteredException**
- Same structure as above

---

### 4. Unit Tests

Implement comprehensive unit tests using NUnit (or your preferred testing framework):

#### Test Cases:
1. **Register_SameCommandTwice_ThrowsHandlerAlreadyRegisteredException**
   - Register a handler for a command
   - Attempt to register the same command again
   - Assert that `HandlerAlreadyRegisteredException` is thrown

2. **Send_CommandWithoutHandler_ThrowsHandlerNotRegisteredException**
   - Attempt to send a command that hasn't been registered
   - Assert that `HandlerNotRegisteredException` is thrown

3. **Register_CommandHandler_SuccessfullyRegisters**
   - Register a handler
   - Assert that no exception is thrown

4. **Send_RegisteredCommand_ExecutesHandlerSuccessfully**
   - Register a handler for a command
   - Send the command with a parameter
   - Assert that the handler executes and returns the expected result

---

### 5. Example Implementation (for testing)

Create concrete test classes: 

#### **ConcreteCommand**
- Implements `ICommand<string, int>`
- Takes a string as input and returns an int

#### **ConcreteCommandHandler**
- Implements `IHandler<ConcreteCommand, string, int>`
- The `Handle` method should return the length of the input string
- Example: Input "Johnny" → Output 6
