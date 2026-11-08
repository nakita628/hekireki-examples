class Membership < ApplicationRecord
  self.table_name = "Membership"
  self.primary_key = %w[userId organizationId]

  attribute :accepted, default: false

  enum :role, { owner: "owner", manager: "manager", member: "member", billing: "billing", dashboard_viewer: "dashboard_viewer" },
       default: :member, validate: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :memberships
  belongs_to :user, foreign_key: "userId", inverse_of: :memberships
end
