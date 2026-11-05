class CleanupThread < ApplicationRecord
  self.table_name = "CleanupThread"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :threadId, presence: true
  validates :archived, inclusion: { in: [ true, false ] }

  belongs_to :job, class_name: "CleanupJob", foreign_key: "jobId", inverse_of: :threads
  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :cleanup_threads
end
