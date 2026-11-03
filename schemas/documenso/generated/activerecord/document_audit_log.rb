class DocumentAuditLog < ApplicationRecord
  self.table_name = "DocumentAuditLog"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :type, presence: true
  validates :data, exclusion: { in: [ nil ] }

  belongs_to :envelope, foreign_key: "envelopeId", optional: true, inverse_of: :audit_logs
end
