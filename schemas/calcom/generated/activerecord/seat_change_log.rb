class SeatChangeLog < ApplicationRecord
  self.table_name = "SeatChangeLog"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt

  enum :changeType, { addition: "ADDITION", removal: "REMOVAL" }, validate: true

  validates :seatCount, presence: true
  validates :monthKey, presence: true
  validates :operationId, uniqueness: { scope: :teamId, allow_nil: true }

  belongs_to :team, foreign_key: "teamId", inverse_of: :seat_change_logs
  belongs_to :proration, class_name: "MonthlyProration", foreign_key: "processedInProrationId", optional: true,
                         inverse_of: :seat_changes
  belongs_to :team_billing, foreign_key: "teamBillingId", optional: true, inverse_of: :seat_change_logs
  belongs_to :organization_billing, foreign_key: "organizationBillingId", optional: true, inverse_of: :seat_change_logs
end
