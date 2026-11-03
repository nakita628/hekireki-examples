use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Account")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    pub provider: String,
    #[sea_orm(column_name = "providerAccountId")]
    pub provider_account_id: String,
    #[sea_orm(column_name = "providerRefreshToken")]
    pub provider_refresh_token: Option<String>,
    #[sea_orm(column_name = "providerAccessToken")]
    pub provider_access_token: Option<String>,
    #[sea_orm(column_name = "providerScope")]
    pub provider_scope: Option<String>,
    #[sea_orm(column_name = "loggedIn", column_type = "TimestampWithTimeZone")]
    pub logged_in: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
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
        if insert && self.logged_in.is_not_set() {
            self.logged_in = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
