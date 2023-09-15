# generarCSV
## Programa para generar Codigos Seguros de Verificacion (CSV)

### Desarrollado por Carlos Clemente (09-2023)

Instrucciones:
- Se puede pasar como parametro un numero entero entre 6 y 10, que sera la longitud de la
  cadena de caracteres que se incluye en el CSV.
- Si no se pasa ningun parametro, la cadena se generará con 8 caracteres.
- El codigo generado tendra 18 caracteres y se incluye una referencia a la fecha y hora 
  generados, ademas de un codigo alfanumerico aleatorio que garantiza la unicidad del codigo final.
- Con una longitud de 8 caracteres en el codigo aleatorio, se generan alrededor de 1,5 billones
  de combinaciones posibles, por lo que aunque se generasen varios codigos en el mismo segundo,
  los caracteres aleatorios haran que sea practicamente imposible que se generen dos CSV iguales.
