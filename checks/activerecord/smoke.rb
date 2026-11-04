# frozen_string_literal: true

# Loads every generated model against the real Active Record API, which `ruby -c` cannot see:
# an association whose class_name resolves to nothing, an enum mapping the DSL rejects, a
# composite primary key the running version does not support. The connection is an empty SQLite
# database in memory; table_name, enum and association reflection all run at class-definition
# time and never query it.
require "active_record"

ActiveRecord::Base.establish_connection(adapter: "sqlite3", database: ":memory:")

models_dir = File.expand_path(ARGV.fetch(0, "models"))

# hekireki-activerecord writes application_record.rb only when the output directory is named
# `models`. These schemas emit to generated/activerecord, so the base class is supplied here,
# the same class `rails new` writes. Require the generated file when one was included.
base = File.join(models_dir, "application_record.rb")
if File.exist?(base)
  require base
else
  class ApplicationRecord < ActiveRecord::Base
    primary_abstract_class
  end
end

model_files = Dir[File.join(models_dir, "*.rb")].sort
model_files.delete(File.expand_path(base))
raise "no generated models found in #{models_dir}" if model_files.empty?

# Column types (prisma_date_time.rb) are referenced from model files. Alphabetical order loads
# api_keys.rb before that type, so require the type files first.
support, models_only = model_files.partition { |file| File.basename(file) == "prisma_date_time.rb" }
(support + models_only).each { |file| require file }

models = ApplicationRecord.descendants
raise "model files loaded but no ApplicationRecord subclasses defined" if models.empty?

associations = models.sum do |model|
  model.reflect_on_all_associations.each do |association|
    # Resolves class_name; a name that is no constant raises here.
    association.klass
  end
  model.defined_enums
  model.reflect_on_all_associations.size
end

puts "ok: #{models.size} models, #{associations} associations"
