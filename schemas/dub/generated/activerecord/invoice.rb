class Invoice < ApplicationRecord
  self.table_name = "Invoice"
  self.inheritance_column = nil

  attribute :amount, default: 0
  attribute :fee, default: 0
  attribute :refundedAmount, default: 0
  attribute :total, default: 0
  attribute :failedAttempts, default: 0
  alias_attribute :created_at, :createdAt

  enum :status, { processing: "processing", completed: "completed", failed: "failed" }, default: :processing,
                                                                                        validate: true
  enum :type, { domain_renewal: "domainRenewal" }, default: :domain_renewal, validate: true
  enum :paymentMethod, { card: "card", ach: "ach", ach_fast: "ach_fast", sepa: "sepa", acss: "acss" },
       validate: { allow_nil: true }

  validates :number, uniqueness: { allow_nil: true }

  belongs_to :workspace, class_name: "Project", foreign_key: "workspaceId", inverse_of: :invoices
end
