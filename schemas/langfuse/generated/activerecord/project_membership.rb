class ProjectMembership < ApplicationRecord
  self.primary_key = %w[project_id user_id]

  enum :role, { owner: "OWNER", admin: "ADMIN", member: "MEMBER", viewer: "VIEWER", none: "NONE", ai_gateway: "AI_GATEWAY" },
       prefix: true, validate: true

  belongs_to :organization_membership, foreign_key: "org_membership_id", inverse_of: :project_memberships
  belongs_to :project, inverse_of: :project_members
  belongs_to :user, inverse_of: :project_memberships
end
