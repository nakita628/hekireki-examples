class WebhookCall < ApplicationRecord
  self.table_name = "WebhookCall"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  enum :status, { success: "SUCCESS", failed: "FAILED" }, validate: true
  enum :event, { document_created: "DOCUMENT_CREATED", document_sent: "DOCUMENT_SENT", document_opened: "DOCUMENT_OPENED", document_signed: "DOCUMENT_SIGNED", document_completed: "DOCUMENT_COMPLETED", document_rejected: "DOCUMENT_REJECTED", document_cancelled: "DOCUMENT_CANCELLED", recipient_expired: "RECIPIENT_EXPIRED", document_recipient_completed: "DOCUMENT_RECIPIENT_COMPLETED", document_reminder_sent: "DOCUMENT_REMINDER_SENT", template_created: "TEMPLATE_CREATED", template_updated: "TEMPLATE_UPDATED", template_deleted: "TEMPLATE_DELETED", template_used: "TEMPLATE_USED" },
       validate: true

  validates :url, presence: true
  validates :requestBody, exclusion: { in: [ nil ] }
  validates :responseCode, presence: true

  belongs_to :webhook, foreign_key: "webhookId", inverse_of: :webhook_calls
end
