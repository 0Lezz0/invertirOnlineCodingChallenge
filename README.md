# InvertirOnline.com Coding Challenge

Bienvenido a la resolución del coding challenge!

### El problema

Tenemos un método que genera un reporte en base a una colección de formas geométricas, procesando algunos datos para presentar información extra. La firma del método es:

```csharp
public static string Imprimir(List<FormaGeometrica> formas, int idioma)
```

Al mismo tiempo, encontramos muy díficil el poder agregar o bien una nueva forma geométrica, o imprimir el reporte en otro idioma. Nos gustaría poder dar soporte para que el usuario pueda agregar otros tipos de formas u obtener el reporte en otros idiomas, pero extender la funcionalidad del código es muy doloroso. ¿Nos podrías dar una mano a refactorear la clase FormaGeometrica? Dentro del código encontrarás un TODO con nuevos requerimientos a satisfacer una vez completada la refactorización.

### La solución

Luego de analizar el problema por un rato, decidí encarar la solución de la siguiente forma:  

    - Utilizar una interface (`IFormaGeometrica`) para modelar un comportamiento general a todas las formas.
    - Crear clases específicas para cada figura, esto es especialmente necesario para resolver formas más complejas (un rectángulo no puede definirse solo con un lado, por ejemplo)
    - La interface nos permite agrupar distintas formas, bajo un mismo estandar (permitiendo, entre muchas cosas más, agrupar todos los distintos tipos de formas en una misma lista)
    - Me resulto útil (a pesar de su mantenimiento) utilizar un enumerador para "listar" las figuras disponibles.
    - Junto a una pequeña clase, el enumerador y un dictionary, resolví el reporte sin neceisdad de conocer a priori las formas disponibles
    - Para el manejo de las traducciones, utilice una clase intermedia (`Translator`) y un archivo de configuración (un sencillo XML); Esto nos permite una flexibilidad muy alta para el manejo de traducciones y nuevos idiomas (por ejemplo, con solo actualizar el XML podemos tener un nuevo idioma funcionando en la aplicación, sin necesidad de tocar código)

**¡¡Gracias por la oportunidad!!**
