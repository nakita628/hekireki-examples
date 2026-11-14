class AuthzUserStanding < ApplicationRecord
  self.table_name = "AuthzUserStanding"
  self.primary_key = "userId"

  validates :standingChangedAt, presence: true
end
