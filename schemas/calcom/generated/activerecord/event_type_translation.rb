class EventTypeTranslation < ApplicationRecord
  self.table_name = "EventTypeTranslation"
  self.primary_key = "uid"
  self.implicit_order_column = "createdAt"

  attribute :uid, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :field, { description: "DESCRIPTION", title: "TITLE" }, validate: true

  validates :sourceLocale, presence: true
  validates :targetLocale, presence: true, uniqueness: { scope: [ :eventTypeId, :field ] }
  validates :translatedText, presence: true

  belongs_to :event_type, foreign_key: "eventTypeId", inverse_of: :field_translations
  belongs_to :creator, class_name: "User", foreign_key: "createdBy", inverse_of: :created_translations
  belongs_to :updater, class_name: "User", foreign_key: "updatedBy", optional: true, inverse_of: :updated_translations
end
