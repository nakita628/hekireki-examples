class Invite < ApplicationRecord
  self.table_name = "Invite"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :teamIds, default: -> { [] }
  alias_attribute :created_at, :createdAt

  enum :role, { owner: "owner", manager: "manager", member: "member", billing: "billing", dashboard_viewer: "dashboard_viewer" },
       default: :member, validate: true

  validates :email, presence: true
  validates :expiresAt, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :invites
  belongs_to :creator, class_name: "User", foreign_key: "creatorId", inverse_of: :invites_created
  belongs_to :acceptor, class_name: "User", foreign_key: "acceptorId", optional: true, inverse_of: :invites_accepted
end
