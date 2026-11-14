defmodule LangWatch.SelfHostedInstanceDiagnostic do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          instance_id: String.t(),
          step_id: String.t(),
          step_description: String.t() | nil,
          release: String.t() | nil,
          attempt: integer(),
          trigger: String.t(),
          byte_length: integer(),
          object_key: String.t(),
          received_at: DateTime.t(),
          expires_at: DateTime.t(),
          slack_announced_at: DateTime.t() | nil
        }

  schema "SelfHostedInstanceDiagnostic" do
    field(:id, :string, primary_key: true)
    field(:instance_id, :string, source: :instanceId)
    field(:step_id, :string, source: :stepId)
    field(:step_description, :string, source: :stepDescription)
    field(:release, :string)
    field(:attempt, :integer)
    field(:trigger, :string)
    field(:byte_length, :integer, source: :byteLength)
    field(:object_key, :string, source: :objectKey)
    field(:received_at, LangWatch.PrismaDateTime, autogenerate: true, source: :receivedAt)
    field(:expires_at, LangWatch.PrismaDateTime, source: :expiresAt)
    field(:slack_announced_at, LangWatch.PrismaDateTime, source: :slackAnnouncedAt)
  end
end