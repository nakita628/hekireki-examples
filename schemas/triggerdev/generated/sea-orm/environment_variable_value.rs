use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "EnvironmentVariableValue")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "valueReferenceId")]
    pub value_reference_id: Option<String>,
    #[sea_orm(column_name = "variableId")]
    pub variable_id: String,
    #[sea_orm(column_name = "environmentId")]
    pub environment_id: String,
    #[sea_orm(column_name = "isSecret", default_value = false)]
    pub is_secret: bool,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(default_value = 1)]
    pub version: i32,
    #[sea_orm(column_name = "lastUpdatedBy")]
    pub last_updated_by: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::secret_reference::Entity",
        from = "Column::ValueReferenceId",
        to = "super::secret_reference::Column::Id",
        on_update = "Cascade",
        on_delete = "SetNull"
    )]
    ValueReference,
    #[sea_orm(
        belongs_to = "super::environment_variable::Entity",
        from = "Column::VariableId",
        to = "super::environment_variable::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Variable,
    #[sea_orm(
        belongs_to = "super::runtime_environment::Entity",
        from = "Column::EnvironmentId",
        to = "super::runtime_environment::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Environment,
}

impl Related<super::secret_reference::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ValueReference.def()
    }
}

impl Related<super::environment_variable::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Variable.def()
    }
}

impl Related<super::runtime_environment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Environment.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
