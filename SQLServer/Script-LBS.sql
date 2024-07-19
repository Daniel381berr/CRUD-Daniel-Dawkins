

--SCRIPT PARA CREAR TABLA
CREATE DATABASE LexBillServices;
GO
USE LexBillServices;


--SCRIPT PARA CREAR LAS TABLAS
CREATE TABLE Clientes (
    ClienteId INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    Apellido NVARCHAR(100),
    Email NVARCHAR(100),
    Telefono NVARCHAR(20),
    Direccion NVARCHAR(200)
);

CREATE TABLE Productos (
    ProductoId INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    CodigoProducto NVARCHAR(50),
    CodigoBarras NVARCHAR(50),
    SKU NVARCHAR(50),
    Precio DECIMAL(18, 2),
    Stock INT
);

CREATE TABLE Pedidos (
    PedidoId INT PRIMARY KEY IDENTITY,
    ClienteId INT FOREIGN KEY REFERENCES Clientes(ClienteId),
    Fecha DATE,
    ITBMS DECIMAL(18, 2),
    Total DECIMAL(18, 2)
);

CREATE TABLE DetallesPedidos (
    DetallePedidoId INT PRIMARY KEY IDENTITY,
    PedidoId INT FOREIGN KEY REFERENCES Pedidos(PedidoId),
    ProductoId INT FOREIGN KEY REFERENCES Productos(ProductoId),
    Cantidad INT,
    Precio DECIMAL(18, 2)
);

CREATE TABLE Facturas (
    FacturaId INT PRIMARY KEY IDENTITY,
    PedidoId INT FOREIGN KEY REFERENCES Pedidos(PedidoId),
    Fecha DATE,
    Total DECIMAL(18, 2)
);

CREATE TABLE TipoCambio (
    TipoCambioId INT PRIMARY KEY IDENTITY,
    Moneda NVARCHAR(3),
    Tasa DECIMAL(18, 4),
    Fecha DATE
);

--SCRIPT DE PROCEDIMIENTO ALMACENADOS--

---PROCEDIMIENTO DE CLIENTES CRUD (CREATE, READER, UPDATE, DELETE)
CREATE PROCEDURE PA_Clientes_CrearCliente
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Email NVARCHAR(100),
    @Telefono NVARCHAR(20),
    @Direccion NVARCHAR(200)
AS
BEGIN
    INSERT INTO Clientes (Nombre, Apellido, Email, Telefono, Direccion)
    VALUES (@Nombre, @Apellido, @Email, @Telefono, @Direccion);
    SELECT SCOPE_IDENTITY() AS ClienteId;
END;


CREATE PROCEDURE PA_Clientes_ListarClientes
AS
BEGIN
    SELECT * FROM Clientes;
END;

CREATE PROCEDURE PA_Clientes_BuscarClientePorId
    @ClienteId INT
AS
BEGIN
    SELECT * FROM Clientes WHERE ClienteId = @ClienteId;
END;

CREATE PROCEDURE PA_Clientes_ModificarCliente
    @ClienteId INT,
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Email NVARCHAR(100),
    @Telefono NVARCHAR(20),
    @Direccion NVARCHAR(200)
AS
BEGIN
    UPDATE Clientes
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        Email = @Email,
        Telefono = @Telefono,
        Direccion = @Direccion
    WHERE ClienteId = @ClienteId;
END;

CREATE PROCEDURE PA_Clientes_EliminarCliente
    @ClienteId INT
AS
BEGIN
    DELETE FROM Clientes WHERE ClienteId = @ClienteId;
END;


--SCRIPT PRODUCTOS PA_Producto_
CREATE PROCEDURE PA_Productos_ListarProductos
AS
BEGIN
    SELECT * FROM Productos;
END;



CREATE PROCEDURE PA_Productos_BuscarProductoPorId
    @ProductoId INT
AS
BEGIN
    SELECT * FROM Productos WHERE ProductoId = @ProductoId;
END;


CREATE PROCEDURE PA_Productos_BuscarProducto
    @Nombre NVARCHAR(100) = NULL,
    @CodigoProducto NVARCHAR(50) = NULL,
    @CodigoBarras NVARCHAR(50) = NULL,
    @SKU NVARCHAR(50) = NULL
AS
BEGIN
    SELECT * FROM Productos
    WHERE (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%')
      AND (@CodigoProducto IS NULL OR CodigoProducto = @CodigoProducto)
      AND (@CodigoBarras IS NULL OR CodigoBarras = @CodigoBarras)
      AND (@SKU IS NULL OR SKU = @SKU);
END;


CREATE PROCEDURE PA_Productos_ModificarProducto
    @ProductoId INT,
    @Nombre NVARCHAR(100),
    @CodigoProducto NVARCHAR(50),
    @CodigoBarras NVARCHAR(50),
    @SKU NVARCHAR(50),
    @Precio DECIMAL(18, 2),
    @Stock INT
AS
BEGIN
    UPDATE Productos
    SET Nombre = @Nombre,
        CodigoProducto = @CodigoProducto,
        CodigoBarras = @CodigoBarras,
        SKU = @SKU,
        Precio = @Precio,
        Stock = @Stock
    WHERE ProductoId = @ProductoId;
END;

CREATE PROCEDURE PA_Productos_EliminarProducto
    @ProductoId INT
AS
BEGIN
    DELETE FROM Productos WHERE ProductoId = @ProductoId;
END;


CREATE PROCEDURE PA_Productos_CrearProducto
    @Nombre NVARCHAR(100),
    @CodigoProducto NVARCHAR(50),
    @CodigoBarras NVARCHAR(50),
    @SKU NVARCHAR(50),
    @Precio DECIMAL(18, 2),
    @Stock INT
AS
BEGIN
    INSERT INTO Productos (Nombre, CodigoProducto, CodigoBarras, SKU, Precio, Stock)
    VALUES (@Nombre, @CodigoProducto, @CodigoBarras, @SKU, @Precio, @Stock);
    SELECT SCOPE_IDENTITY() AS ProductoId;
END;

----SCRIPT DE PEDIDOS CRUD PA_Pedidos_
CREATE PROCEDURE PA_Pedidos_ListarPedidos
AS
BEGIN
    SELECT * FROM Pedidos;
END;

CREATE PROCEDURE PA_Pedidos_BuscarPedidoPorId
    @PedidoId INT
AS
BEGIN
    SELECT * FROM Pedidos WHERE PedidoId = @PedidoId;
END;


CREATE PROCEDURE PA_Pedidos_ModificarPedido
    @PedidoId INT,
    @ClienteId INT,
    @Fecha DATE,
    @ITBMS DECIMAL(18, 2),
    @Total DECIMAL(18, 2)
AS
BEGIN
    UPDATE Pedidos
    SET ClienteId = @ClienteId,
        Fecha = @Fecha,
        ITBMS = @ITBMS,
        Total = @Total
    WHERE PedidoId = @PedidoId;
END;


CREATE PROCEDURE PA_Pedidos_EliminarPedido
    @PedidoId INT
AS
BEGIN
    DELETE FROM Pedidos WHERE PedidoId = @PedidoId;
END;


CREATE PROCEDURE PA_Pedidos_CrearPedido
    @ClienteId INT,
    @Fecha DATE,
    @ITBMS DECIMAL(18, 2),
    @Total DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO Pedidos (ClienteId, Fecha, ITBMS, Total)
    VALUES (@ClienteId, @Fecha, @ITBMS, @Total);
    SELECT SCOPE_IDENTITY() AS PedidoId;
END;

--SCRIPT DE FACTURAS PA_Facturas_
CREATE PROCEDURE PA_Facturas_ListarFacturas
AS
BEGIN
    SELECT * FROM Facturas;
END;

CREATE PROCEDURE PA_Facturas_BuscarFacturaPorId
    @FacturaId INT
AS
BEGIN
    SELECT * FROM Facturas WHERE FacturaId = @FacturaId;
END;

CREATE PROCEDURE PA_Facturas_ModificarFactura
    @FacturaId INT,
    @PedidoId INT,
    @Fecha DATE,
    @Total DECIMAL(18, 2)
AS
BEGIN
    UPDATE Facturas
    SET PedidoId = @PedidoId,
        Fecha = @Fecha,
        Total = @Total
    WHERE FacturaId = @FacturaId;
END;


CREATE PROCEDURE PA_Facturas_EliminarFactura
    @FacturaId INT
AS
BEGIN
    DELETE FROM Facturas WHERE FacturaId = @FacturaId;
END;


CREATE PROCEDURE PA_Facturas_CrearFactura
    @PedidoId INT,
    @Fecha DATE,
    @Total DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO Facturas (PedidoId, Fecha, Total)
    VALUES (@PedidoId, @Fecha, @Total);
    SELECT SCOPE_IDENTITY() AS FacturaId;
END;




--SCRIPT TIPO DE CAMBIO PA_TipoCambio_

CREATE PROCEDURE PA_TipoCambio_ListarTiposCambio
AS
BEGIN
    SELECT * FROM TipoCambio;
END;


CREATE PROCEDURE PA_TipoCambio_BuscarTipoCambio
    @Moneda NVARCHAR(3),
    @Fecha DATE
AS
BEGIN
    SELECT * FROM TipoCambio WHERE Moneda = @Moneda AND Fecha = @Fecha;
END;

CREATE PROCEDURE PA_TipoCambio_ModificarTipoCambio
    @TipoCambioId INT,
    @Moneda NVARCHAR(3),
    @Tasa DECIMAL(18, 4),
    @Fecha DATE
AS
BEGIN
    UPDATE TipoCambio
    SET Moneda = @Moneda,
        Tasa = @Tasa,
        Fecha = @Fecha
    WHERE TipoCambioId = @TipoCambioId;
END;

CREATE PROCEDURE PA_TipoCambio_EliminarTipoCambio
    @TipoCambioId INT
AS
BEGIN
    DELETE FROM TipoCambio WHERE TipoCambioId = @TipoCambioId;
END;


CREATE PROCEDURE PA_TipoCambio_CrearTipoCambio
    @Moneda NVARCHAR(3),
    @Tasa DECIMAL(18, 4),
    @Fecha DATE
AS
BEGIN
    INSERT INTO TipoCambio (Moneda, Tasa, Fecha)
    VALUES (@Moneda, @Tasa, @Fecha);
    SELECT SCOPE_IDENTITY() AS TipoCambioId;
END;


----SCRIPT DETALLE PEDIDO PA_DetallesPedidos_
CREATE PROCEDURE PA_DetallesPedidos_CrearDetallePedido
    @PedidoId INT,
    @ProductoId INT,
    @Cantidad INT,
    @Precio DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO DetallesPedidos (PedidoId, ProductoId, Cantidad, Precio)
    VALUES (@PedidoId, @ProductoId, @Cantidad, @Precio);
    SELECT SCOPE_IDENTITY() AS DetallePedidoId;
END;

CREATE PROCEDURE PA_DetallesPedidos_ModificarDetallePedido
    @DetallePedidoId INT,
    @PedidoId INT,
    @ProductoId INT,
    @Cantidad INT,
    @Precio DECIMAL(18, 2)
AS
BEGIN
    UPDATE DetallesPedidos
    SET PedidoId = @PedidoId,
        ProductoId = @ProductoId,
        Cantidad = @Cantidad,
        Precio = @Precio
    WHERE DetallePedidoId = @DetallePedidoId;
END;


CREATE PROCEDURE PA_DetallesPedidos_EliminarDetallePedido
    @DetallePedidoId INT
AS
BEGIN
    DELETE FROM DetallesPedidos WHERE DetallePedidoId = @DetallePedidoId;
END;


--INSERT INTO [dbo].[Clientes] ([Nombre],[Apellido],[Email],[Telefono],[Direccion])
--VALUES('Ezequiel', 'Dawkins', 'daniygenesis@seamanmucho.es','6578-9903','Puerto Escondido Rantan de Maliente')

SELECT *
FROM [dbo].[Clientes]

SELECT * FROM Productos;







