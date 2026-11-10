class ContactFormMessage < ApplicationRecord
  self.table_name = "ContactFormMessage"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :isRead, default: false
  alias_attribute :created_at, :createdAt

  validates :content, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :contact_form_messages
end
