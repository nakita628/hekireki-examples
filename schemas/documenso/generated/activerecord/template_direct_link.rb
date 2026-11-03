class TemplateDirectLink < ApplicationRecord
  self.table_name = "TemplateDirectLink"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :envelopeId, uniqueness: true
  validates :token, presence: true, uniqueness: true
  validates :enabled, inclusion: { in: [ true, false ] }
  validates :directTemplateRecipientId, presence: true

  belongs_to :envelope, foreign_key: "envelopeId", inverse_of: :direct_link
end
