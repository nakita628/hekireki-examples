class CleanupJob < ApplicationRecord
  self.table_name = "CleanupJob"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :daysOld, default: 7
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :action, { archive: "ARCHIVE", mark_read: "MARK_READ" }, default: :archive, validate: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :cleanup_jobs
  has_many :threads, class_name: "CleanupThread", foreign_key: "jobId", inverse_of: :job, dependent: :destroy
end
