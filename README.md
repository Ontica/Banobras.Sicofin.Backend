# Sistema de Contabilidad Financiera. SICOFIN. Banobras. 2021.

La actualización del Sistema de Contabilidad Financiera (SICOFIN) está siendo construida 
a la medida de las necesidades del Banco Nacional de Obras y Servicios Públicos, S.N.C. 
(Banobras), tomando como base el diseño del mismo sistema desarrollado entre los años 
2000 y 2002 para Banobras por nuestra organización.

El proyecto está siendo desarrollado con C# 8.0, .NET Framework 4.8, ASP .NET, utilizando
como base diversos componentes de Empiria Framework y de Empiria Financial Accounting.

Al igual que la versión anterior, el sistema utiliza Oracle como servidor de base de datos,
y respeta el diseño de las mismas para evitar impactos indeseados en otros sistemas del Banco.

Este repositorio aglutina los componentes del backend del sistema y contiene código específico
para la individualización del sistema a las necesidades actuales de Banobras.


## Contenido

1. **App Services**  
   Casos de uso con las reglas de negocio específicas para Banobras.


2. **Web Api**  
   Consolida todas las web apis, para acceder a todos los casos de uso y componentes
   de dominio utilizados por el sistema SICOFIN.


3. **Database Scripts**  
   Scripts con la definición de tablas, stored procedures, y otros elementos
   de la base de datos Oracle.


## Documentación

De acuerdo a las prácticas de desarrollo ágil, se escribirá e incluirá en este
repositorio en las últimas semanas del proyecto.


## Licencia

Este sistema se distribuye bajo la licencia [GNU AFFERO GENERAL PUBLIC LICENSE](https://github.com/Ontica/Sicofin/blob/master/LICENSE.txt).

En Óntica siempre entregamos soluciones y sistemas de información de código abierto.
Consideramos que esta práctica es la correcta, especialmente cuando se trata de
sistemas de utilidad pública, como es el caso de los sistemas para gobierno.


## Copyright

Copyright © 2000-2021. La Vía Óntica S.C. y colaboradores.

