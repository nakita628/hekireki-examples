defmodule Postiz.MastraObservationalMemory do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          lookup_key: String.t(),
          scope: String.t(),
          resource_id: String.t() | nil,
          thread_id: String.t() | nil,
          active_observations: String.t(),
          active_observations_pending_update: String.t() | nil,
          origin_type: String.t(),
          config: String.t(),
          generation_count: integer(),
          last_observed_at: DateTime.t() | nil,
          last_reflection_at: DateTime.t() | nil,
          pending_message_tokens: integer(),
          total_tokens_observed: integer(),
          observation_token_count: integer(),
          is_observing: boolean(),
          is_reflecting: boolean(),
          observed_message_ids: map() | nil,
          observed_timezone: String.t() | nil,
          buffered_observations: String.t() | nil,
          buffered_observation_tokens: integer() | nil,
          buffered_message_ids: map() | nil,
          buffered_reflection: String.t() | nil,
          buffered_reflection_tokens: integer() | nil,
          buffered_reflection_input_tokens: integer() | nil,
          reflected_observation_line_count: integer() | nil,
          buffered_observation_chunks: map() | nil,
          is_buffering_observation: boolean(),
          is_buffering_reflection: boolean(),
          last_buffered_at_tokens: integer(),
          last_buffered_at_time: DateTime.t() | nil,
          metadata: map() | nil,
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          last_observed_at_z: DateTime.t() | nil,
          last_reflection_at_z: DateTime.t() | nil,
          last_buffered_at_time_z: DateTime.t() | nil,
          created_at_z: DateTime.t() | nil,
          updated_at_z: DateTime.t() | nil
        }

  schema "mastra_observational_memory" do
    field(:id, :string, primary_key: true)
    field(:lookup_key, :string, source: :lookupKey)
    field(:scope, :string)
    field(:resource_id, :string, source: :resourceId)
    field(:thread_id, :string, source: :threadId)
    field(:active_observations, :string, source: :activeObservations)
    field(:active_observations_pending_update, :string, source: :activeObservationsPendingUpdate)
    field(:origin_type, :string, source: :originType)
    field(:config, :string)
    field(:generation_count, :integer, source: :generationCount)
    field(:last_observed_at, Postiz.PrismaDateTime, source: :lastObservedAt)
    field(:last_reflection_at, Postiz.PrismaDateTime, source: :lastReflectionAt)
    field(:pending_message_tokens, :integer, source: :pendingMessageTokens)
    field(:total_tokens_observed, :integer, source: :totalTokensObserved)
    field(:observation_token_count, :integer, source: :observationTokenCount)
    field(:is_observing, :boolean, source: :isObserving)
    field(:is_reflecting, :boolean, source: :isReflecting)
    field(:observed_message_ids, :map, source: :observedMessageIds)
    field(:observed_timezone, :string, source: :observedTimezone)
    field(:buffered_observations, :string, source: :bufferedObservations)
    field(:buffered_observation_tokens, :integer, source: :bufferedObservationTokens)
    field(:buffered_message_ids, :map, source: :bufferedMessageIds)
    field(:buffered_reflection, :string, source: :bufferedReflection)
    field(:buffered_reflection_tokens, :integer, source: :bufferedReflectionTokens)
    field(:buffered_reflection_input_tokens, :integer, source: :bufferedReflectionInputTokens)
    field(:reflected_observation_line_count, :integer, source: :reflectedObservationLineCount)
    field(:buffered_observation_chunks, :map, source: :bufferedObservationChunks)
    field(:is_buffering_observation, :boolean, source: :isBufferingObservation)
    field(:is_buffering_reflection, :boolean, source: :isBufferingReflection)
    field(:last_buffered_at_tokens, :integer, source: :lastBufferedAtTokens)
    field(:last_buffered_at_time, Postiz.PrismaDateTime, source: :lastBufferedAtTime)
    field(:metadata, :map)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:updated_at, Postiz.PrismaDateTime, source: :updatedAt)
    field(:last_observed_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :lastObservedAtZ)
    field(:last_reflection_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :lastReflectionAtZ)
    field(:last_buffered_at_time_z, Postiz.PrismaDateTime, autogenerate: true, source: :lastBufferedAtTimeZ)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
    field(:updated_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :updatedAtZ)
  end
end