use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "mastra_scorer_definition_versions")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "scorerDefinitionId")]
    pub scorer_definition_id: String,
    #[sea_orm(column_name = "versionNumber")]
    pub version_number: i32,
    pub name: String,
    pub description: Option<String>,
    pub r#type: String,
    pub model: Option<Json>,
    pub instructions: Option<String>,
    #[sea_orm(column_name = "scoreRange")]
    pub score_range: Option<Json>,
    #[sea_orm(column_name = "presetConfig")]
    pub preset_config: Option<Json>,
    #[sea_orm(column_name = "defaultSampling")]
    pub default_sampling: Option<Json>,
    #[sea_orm(column_name = "changedFields")]
    pub changed_fields: Option<Json>,
    #[sea_orm(column_name = "changeMessage")]
    pub change_message: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "createdAtZ", column_type = "TimestampWithTimeZone")]
    pub created_at_z: Option<DateTimeWithTimeZone>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at_z.is_not_set() {
            self.created_at_z = Set(Some(now.fixed_offset()));
        }
        Ok(self)
    }
}
