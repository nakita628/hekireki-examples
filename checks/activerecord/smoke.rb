# frozen_string_literal: true

# Loads every generated model against the real Active Record API, which `ruby -c` cannot see:
# an association whose class_name resolves to nothing, an enum mapping the DSL rejects, a
# composite primary key the running version does not support. The connection is an empty SQLite
# database in memory; table_name, enum and association reflection all run at class-definition
# time and never query it.
require "active_record"

ActiveRecord::Base.establish_connection(adapter: "sqlite3", database: ":memory:")

models_dir = File.expand_path(ARGV.fetch(0, "models"))
base = File.join(models_dir, "application_record.rb")
raise "application_record.rb expected in #{models_dir}" unless File.exist?(base)

require base
model_files = (Dir[File.join(models_dir, "*.rb")] - [base]).sort
raise "no generated models found in #{models_dir}" if model_files.empty?

model_files.each { |file| require file }

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
