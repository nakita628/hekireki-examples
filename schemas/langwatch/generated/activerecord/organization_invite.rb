class OrganizationInvite < ApplicationRecord
  self.table_name = "OrganizationInvite"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", accepted: "ACCEPTED", waiting_approval: "WAITING_APPROVAL", payment_pending: "PAYMENT_PENDING", revoked: "REVOKED" },
       default: :pending, validate: true
  enum :role, { admin: "ADMIN", member: "MEMBER", external: "EXTERNAL", developer: "DEVELOPER" }, validate: true

  validates :email, presence: true
  validates :inviteCode, presence: true, uniqueness: true
  validates :teamIds, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :organization_invite
  belongs_to :requested_by_user, class_name: "User", foreign_key: "requestedBy", optional: true,
                                 inverse_of: :invite_requests
end
