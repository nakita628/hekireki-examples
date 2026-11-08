class Event < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt

  validates :name, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :events
  belongs_to :contact, foreign_key: "contactId", optional: true, inverse_of: :events
  belongs_to :email, foreign_key: "emailId", optional: true, inverse_of: :events
end
