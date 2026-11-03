class TagsOnResponses < ApplicationRecord
  self.table_name = "TagsOnResponses"
  self.primary_key = %w[responseId tagId]

  belongs_to :response, foreign_key: "responseId", inverse_of: :tags
  belongs_to :tag, foreign_key: "tagId", inverse_of: :responses
end
