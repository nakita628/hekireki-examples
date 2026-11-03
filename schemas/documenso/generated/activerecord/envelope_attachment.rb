class EnvelopeAttachment < ApplicationRecord
  self.table_name = "EnvelopeAttachment"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :type, presence: true
  validates :label, presence: true
  validates :data, presence: true

  belongs_to :envelope, foreign_key: "envelopeId", inverse_of: :envelope_attachments
end
