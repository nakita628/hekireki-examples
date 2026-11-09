use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ProfileBuilderFunction")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "profileBuilderId")]
    pub profile_builder_id: String,
    #[sea_orm(column_name = "functionId")]
    pub function_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::profile_builder::Entity",
        from = "Column::ProfileBuilderId",
        to = "super::profile_builder::Column::Id"
    )]
    ProfileBuilder,
    #[sea_orm(
        belongs_to = "super::configuration_object::Entity",
        from = "Column::FunctionId",
        to = "super::configuration_object::Column::Id"
    )]
    Function,
}

impl Related<super::profile_builder::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ProfileBuilder.def()
    }
}

impl Related<super::configuration_object::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Function.def()
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
