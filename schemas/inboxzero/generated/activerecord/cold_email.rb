class ColdEmail < ApplicationRecord
  self.table_name = "ColdEmail"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { ai_labeled_cold: "AI_LABELED_COLD", user_rejected_cold: "USER_REJECTED_COLD" },
       validate: { allow_nil: true }

  validates :fromEmail, presence: true, uniqueness: { scope: :emailAccountId }

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :cold_emails
  has_many :digest_items, foreign_key: "coldEmailId", inverse_of: :cold_email, dependent: :nullify
end
