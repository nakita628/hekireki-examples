use super::attribute_type::AttributeType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Attribute")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "teamId")]
    pub team_id: i32,
    pub r#type: AttributeType,
    pub name: String,
    pub slug: String,
    #[sea_orm(default_value = true)]
    pub enabled: bool,
    #[sea_orm(column_name = "usersCanEditRelation", default_value = false)]
    pub users_can_edit_relation: bool,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "isWeightsEnabled", default_value = false)]
    pub is_weights_enabled: bool,
    #[sea_orm(column_name = "isLocked", default_value = false)]
    pub is_locked: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Team,
    #[sea_orm(has_many = "super::attribute_option::Entity")]
    Options,
    #[sea_orm(has_many = "super::attribute_sync_field_mapping::Entity")]
    AttributeSyncFieldMappings,
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl Related<super::attribute_option::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Options.def()
    }
}

impl Related<super::attribute_sync_field_mapping::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AttributeSyncFieldMappings.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

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
