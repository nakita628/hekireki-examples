class AssignmentReason < ApplicationRecord
  self.table_name = "AssignmentReason"

  alias_attribute :created_at, :createdAt

  enum :reasonEnum, { reassigned: "REASSIGNED", rr_reassigned: "RR_REASSIGNED", rerouted: "REROUTED", salesforce_assignment: "SALESFORCE_ASSIGNMENT" },
       validate: true

  validates :reasonString, presence: true

  belongs_to :booking, foreign_key: "bookingId", inverse_of: :assignment_reason
end
