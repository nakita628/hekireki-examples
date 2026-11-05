class Invite < ApplicationRecord
  attribute :projectAccess, default: -> { JSON.parse("[]") }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :projectScope, { all: "all", selected: "selected" }, default: :all, prefix: true, validate: true

  validates :email, presence: true
  validates :expiresAt, presence: true
  validates :role, presence: true

  belongs_to :created_by, class_name: "User", foreign_key: "createdById", inverse_of: :invites
  belongs_to :organization, foreign_key: "organizationId", inverse_of: :invites
end
