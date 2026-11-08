class Membership < ApplicationRecord
  self.primary_key = %w[userId projectId]

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :role, { owner: "OWNER", admin: "ADMIN", member: "MEMBER" }, default: :member, validate: true

  belongs_to :user, foreign_key: "userId", inverse_of: :memberships
  belongs_to :project, foreign_key: "projectId", inverse_of: :members
end
