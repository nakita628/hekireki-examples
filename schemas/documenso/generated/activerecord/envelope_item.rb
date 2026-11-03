class EnvelopeItem < ApplicationRecord
  self.table_name = "EnvelopeItem"

  validates :title, presence: true
  validates :order, presence: true
  validates :documentDataId, uniqueness: true
  validates :documentDataId, uniqueness: true

  belongs_to :document_data, foreign_key: "documentDataId", inverse_of: :envelope_item
  belongs_to :envelope, foreign_key: "envelopeId", inverse_of: :envelope_items
  has_many :field, foreign_key: "envelopeItemId", inverse_of: :envelope_item, dependent: :destroy
end
