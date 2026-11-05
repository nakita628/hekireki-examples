class ClassificationFeedback < ApplicationRecord
  self.table_name = "ClassificationFeedback"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  enum :eventType, { label_added: "LABEL_ADDED", label_removed: "LABEL_REMOVED" }, validate: true

  validates :sender, presence: true, uniqueness: { scope: [ :emailAccountId, :ruleId, :messageId, :eventType ] }
  validates :threadId, presence: true
  validates :messageId, presence: true

  belongs_to :rule, foreign_key: "ruleId", inverse_of: :classification_feedback
  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :classification_feedback
end
