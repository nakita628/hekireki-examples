class WorkspaceInvitation < ApplicationRecord
  self.table_name = "WorkspaceInvitation"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { admin: "ADMIN", member: "MEMBER", guest: "GUEST" }, validate: true

  validates :email, presence: true

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :invitations
end
