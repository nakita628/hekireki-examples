use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "InvitedUsers")]
pub struct Model {
    #[sea_orm(column_name = "adminUid")]
    pub admin_uid: String,
    #[sea_orm(column_name = "adminEmail")]
    pub admin_email: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(unique)]
    #[sea_orm(column_name = "inviteeEmail")]
    pub invitee_email: String,
    #[sea_orm(column_name = "invitedOn", column_type = "TimestampWithTimeZone")]
    pub invited_on: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::AdminUid",
        to = "super::user::Column::Uid",
        on_delete = "Cascade"
    )]
    User,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.invited_on.is_not_set() {
            self.invited_on = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
