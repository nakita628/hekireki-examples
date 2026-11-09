class InvitationToken < ApplicationRecord
  self.table_name = "InvitationToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :role, { owner: "owner", editor: "editor", analyst: "analyst" }, default: :owner, validate: true

  validates :workspaceId, presence: true
  validates :email, presence: true
  validates :token, presence: true
end
