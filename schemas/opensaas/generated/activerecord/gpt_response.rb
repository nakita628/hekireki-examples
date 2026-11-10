class GptResponse < ApplicationRecord
  self.table_name = "GptResponse"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :content, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :gpt_responses
end
