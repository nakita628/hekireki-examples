class RoleAssignment < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  enum :system_role, { owner: "OWNER", admin: "ADMIN", member: "MEMBER", viewer: "VIEWER", none: "NONE", legacy_project_api_key: "LEGACY_PROJECT_API_KEY", legacy_organization_api_key: "LEGACY_ORGANIZATION_API_KEY", scores_ingest: "SCORES_INGEST", ingest: "INGEST", ai_gateway: "AI_GATEWAY" },
       prefix: true, validate: true

  validates :owner_id, presence: true, uniqueness: { scope: %i[principal_id role_id] }
  validates :principal_id, presence: true
  validates :role_id, presence: true

  belongs_to :project, optional: true, inverse_of: :role_assignments
  belongs_to :organization, foreign_key: "org_id", inverse_of: :role_assignments
  belongs_to :user, optional: true, inverse_of: :role_assignments
  belongs_to :api_key, optional: true, inverse_of: :role_assignments
end
