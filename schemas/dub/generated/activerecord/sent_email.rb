class SentEmail < ApplicationRecord
  self.table_name = "SentEmail"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :type, presence: true

  belongs_to :project, foreign_key: "projectId", optional: true, inverse_of: :sent_emails
end
