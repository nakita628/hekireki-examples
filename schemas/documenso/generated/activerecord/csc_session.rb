class CscSession < ApplicationRecord
  self.table_name = "CscSession"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :envelopeId, presence: true
  validates :signingTime, presence: true
  validates :itemsJson, exclusion: { in: [ nil ] }
  validates :recipientId, uniqueness: true

  belongs_to :recipient, foreign_key: "recipientId", inverse_of: :csc_session
end
