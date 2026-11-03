class MonthlyProration < ApplicationRecord
  self.table_name = "MonthlyProration"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :retryCount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", invoice_created: "INVOICE_CREATED", charged: "CHARGED", failed: "FAILED", cancelled: "CANCELLED" },
       default: :pending, validate: true

  validates :monthKey, presence: true, uniqueness: { scope: :teamId }
  validates :periodStart, presence: true
  validates :periodEnd, presence: true
  validates :seatsAtStart, presence: true
  validates :seatsAdded, presence: true
  validates :seatsRemoved, presence: true
  validates :netSeatIncrease, presence: true
  validates :seatsAtEnd, presence: true
  validates :subscriptionId, presence: true
  validates :subscriptionItemId, presence: true
  validates :customerId, presence: true
  validates :subscriptionStart, presence: true
  validates :subscriptionEnd, presence: true
  validates :remainingDays, presence: true
  validates :pricePerSeat, presence: true
  validates :proratedAmount, presence: true

  belongs_to :team, foreign_key: "teamId", inverse_of: :monthly_prorations
  belongs_to :team_billing, foreign_key: "teamBillingId", optional: true, inverse_of: :monthly_prorations
  belongs_to :organization_billing, foreign_key: "organizationBillingId", optional: true,
                                    inverse_of: :monthly_prorations
  has_many :seat_changes, class_name: "SeatChangeLog", foreign_key: "processedInProrationId", inverse_of: :proration,
                          dependent: :nullify
end
