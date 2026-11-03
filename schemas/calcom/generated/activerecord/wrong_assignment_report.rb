class WrongAssignmentReport < ApplicationRecord
  self.table_name = "WrongAssignmentReport"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", reviewed: "REVIEWED", resolved: "RESOLVED", dismissed: "DISMISSED" },
       default: :pending, validate: true

  validates :bookingUid, uniqueness: true
  validates :additionalNotes, presence: true

  belongs_to :booking, foreign_key: "bookingUid", primary_key: "uid", inverse_of: :wrong_assignment_reports
  belongs_to :reported_by, class_name: "User", foreign_key: "reportedById", optional: true,
                           inverse_of: :wrong_assignment_reports
  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :wrong_assignment_reports
  belongs_to :reviewed_by, class_name: "User", foreign_key: "reviewedById", optional: true,
                           inverse_of: :reviewed_wrong_assignment_reports
end
