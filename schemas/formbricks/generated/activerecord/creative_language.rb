class CreativeLanguage < ApplicationRecord
  self.table_name = "CreativeLanguage"
  self.primary_key = %w[languageId creativeId]

  attribute :default, default: false
  attribute :enabled, default: true

  belongs_to :language, foreign_key: "languageId", inverse_of: :creative_languages
  belongs_to :creative, foreign_key: "creativeId", inverse_of: :languages
end
