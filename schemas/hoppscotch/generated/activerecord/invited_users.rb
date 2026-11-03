class InvitedUsers < ApplicationRecord
  self.table_name = "InvitedUsers"

  validates :adminEmail, presence: true
  validates :inviteeEmail, presence: true, uniqueness: true

  belongs_to :user, foreign_key: "adminUid", inverse_of: :invited_users
end
