class GatewayBudgetLedger < ApplicationRecord
  self.table_name = "GatewayBudgetLedger"

  attribute :id, default: -> { Nanoid.generate }
  attribute :tokensInput, default: 0
  attribute :tokensOutput, default: 0
  attribute :tokensCacheRead, default: 0
  attribute :tokensCacheWrite, default: 0

  enum :status, { success: "SUCCESS", provider_error: "PROVIDER_ERROR", blocked_by_guardrail: "BLOCKED_BY_GUARDRAIL", cancelled: "CANCELLED" },
       default: :success, validate: true

  validates :gatewayRequestId, presence: true, uniqueness: { scope: :budgetId }
  validates :amountUsd, presence: true
  validates :model, presence: true

  belongs_to :budget, class_name: "GatewayBudget", foreign_key: "budgetId", inverse_of: :ledger_entries
  belongs_to :virtual_key, foreign_key: "virtualKeyId", inverse_of: :ledger_entries
end
