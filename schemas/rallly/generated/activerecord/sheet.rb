class Sheet < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :deleted, default: false

  validates :title, presence: true
  validates :url_id, presence: true, uniqueness: true

  belongs_to :space, inverse_of: :sheets
  belongs_to :host, class_name: "User", inverse_of: :hosted_sheets
  has_many :slots, class_name: "SheetSlot", inverse_of: :sheet, dependent: :destroy
end
