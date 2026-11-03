class PlatformBilling < ApplicationRecord
  self.table_name = "PlatformBilling"

  attribute :plan, default: "none"
  attribute :overdue, default: false

  validates :customerId, presence: true

  belongs_to :manager_billing, class_name: "PlatformBilling", foreign_key: "managerBillingId", optional: true,
                               inverse_of: :managed_billings
  belongs_to :team, foreign_key: "id", inverse_of: :platform_billing
  has_many :managed_billings, class_name: "PlatformBilling", foreign_key: "managerBillingId",
                              inverse_of: :manager_billing, dependent: :nullify
end
