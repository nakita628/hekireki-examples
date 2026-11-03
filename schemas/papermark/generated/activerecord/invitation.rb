class Invitation < ApplicationRecord
  self.table_name = "Invitation"

  attribute :dataroomIds, default: -> { [] }
  alias_attribute :created_at, :createdAt

  enum :role, { admin: "ADMIN", manager: "MANAGER", member: "MEMBER", dataroom_member: "DATAROOM_MEMBER" },
       default: :member, validate: true

  validates :email, presence: true, uniqueness: { scope: :teamId }
  validates :expires, presence: true
  validates :token, presence: true, uniqueness: true

  belongs_to :team, foreign_key: "teamId", inverse_of: :invitations
end
