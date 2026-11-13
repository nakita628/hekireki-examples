class BulkInboxActionJob < ApplicationRecord
  self.table_name = "BulkInboxActionJob"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :recoveryCount, default: 0
  attribute :processedCount, default: 0
  attribute :matchedCount, default: 0
  attribute :actedCount, default: 0
  attribute :skippedCount, default: 0
  attribute :failedCount, default: 0
  attribute :limitReached, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { running: "RUNNING", completed: "COMPLETED", failed: "FAILED", cancelled: "CANCELLED" },
       default: :running, validate: true

  validates :search, exclusion: { in: [ nil ] }
  validates :action, presence: true
  validates :confirmationKey, presence: true, uniqueness: { scope: :emailAccountId }

  belongs_to :chat, foreign_key: "chatId", optional: true, inverse_of: :bulk_inbox_action_jobs
  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :bulk_inbox_action_jobs
  has_many :handled_threads, class_name: "BulkInboxActionThread", foreign_key: "jobId", inverse_of: :job,
                             dependent: :destroy
end
