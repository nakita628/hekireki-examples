class OrganizationBilling < ApplicationRecord
  self.table_name = "OrganizationBilling"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :billingPeriod, { monthly: "MONTHLY", annually: "ANNUALLY" }, validate: { allow_nil: true }
  enum :billingMode, { seats: "SEATS", active_users: "ACTIVE_USERS" }, default: :seats, validate: true

  validates :teamId, uniqueness: true
  validates :subscriptionId, presence: true, uniqueness: true
  validates :subscriptionItemId, presence: true
  validates :customerId, presence: true
  validates :status, presence: true
  validates :planName, presence: true

  belongs_to :team, foreign_key: "teamId", inverse_of: :organization_billing
  has_many :monthly_prorations, foreign_key: "organizationBillingId", inverse_of: :organization_billing,
                                dependent: :nullify
  has_many :seat_change_logs, foreign_key: "organizationBillingId", inverse_of: :organization_billing,
                              dependent: :nullify
end
