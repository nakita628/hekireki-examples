use super::contact_attribute_data_type::ContactAttributeDataType;
use super::contact_attribute_type::ContactAttributeType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ContactAttributeKey")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    #[sea_orm(column_name = "isUnique", default_value = false)]
    pub is_unique: bool,
    pub key: String,
    pub name: Option<String>,
    pub description: Option<String>,
    #[sea_orm(default_value = "custom")]
    pub r#type: ContactAttributeType,
    #[sea_orm(column_name = "dataType", default_value = "string")]
    pub data_type: ContactAttributeDataType,
    #[sea_orm(column_name = "workspaceId")]
    pub workspace_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::workspace::Entity",
        from = "Column::WorkspaceId",
        to = "super::workspace::Column::Id",
        on_delete = "Cascade"
    )]
    Workspace,
    #[sea_orm(has_many = "super::contact_attribute::Entity")]
    Attributes,
    #[sea_orm(has_many = "super::survey_attribute_filter::Entity")]
    AttributeFilters,
}

impl Related<super::workspace::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workspace.def()
    }
}

impl Related<super::contact_attribute::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Attributes.def()
    }
}

impl Related<super::survey_attribute_filter::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AttributeFilters.def()
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
