class Contact < ApplicationRecord
  self.table_name = "Contact"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :contacts
  has_many :responses, foreign_key: "contactId", inverse_of: :contact, dependent: :destroy
  has_many :contact_attributes, foreign_key: "contactId", inverse_of: :contact, dependent: :destroy
  has_many :displays, foreign_key: "contactId", inverse_of: :contact, dependent: :destroy
end
