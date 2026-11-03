class MemberInWorkspace < ApplicationRecord
  self.table_name = "MemberInWorkspace"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :role, { admin: "ADMIN", member: "MEMBER", guest: "GUEST" }, validate: true

  validates :userId, uniqueness: { scope: :workspaceId }

  belongs_to :user, foreign_key: "userId", inverse_of: :workspaces
  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :members
end
