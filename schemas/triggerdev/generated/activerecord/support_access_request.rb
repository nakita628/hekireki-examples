class SupportAccessRequest < ApplicationRecord
  self.table_name = "SupportAccessRequest"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", approved: "APPROVED", cancelled: "CANCELLED" }, default: :pending, validate: true

  validates :reason, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :support_access_requests
  belongs_to :requested_by, class_name: "User", foreign_key: "requestedById", optional: true,
                            inverse_of: :support_access_requests_made
  belongs_to :approved_by, class_name: "User", foreign_key: "approvedById", optional: true,
                           inverse_of: :support_access_requests_approved
end
